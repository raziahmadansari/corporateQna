import { Component, OnDestroy, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Editor, Toolbar } from 'ngx-editor';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';
import { fromEvent } from 'rxjs';
import { debounceTime, map, distinctUntilChanged, filter } from "rxjs/operators";

import { Question } from 'src/app/shared/models/question';
import { Category } from 'src/app/shared/models/category';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Solution } from 'src/app/shared/models/solution';
import { LikeDislike } from 'src/app/shared/models/like-dislike';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [ ]
})
export class HomeComponent implements OnInit, OnDestroy {
  allQuestions: Question[];
  filteredQuestions: Question[];
  questions: Question[];
  solutions: Solution[];
  categories: Category[];
  activeQuestionId: Number = 0;
  activeQuestion: Question;

  editorSm = true;
  editor: Editor;
  toolbar: Toolbar = [
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['bold', 'italic', 'underline'],
    ['ordered_list', 'bullet_list'],
    ['blockquote', 'link'],
  ];
  html = '';
  placeholder = 'Enter you answer here';
  editorModal: Editor;
  htmlQuestionDescription: '';
  placeholderQuestionModal = 'Enter your text here'

  closeResult = '';
  @ViewChild('questionSearchInput', { static: true }) questionSearchInput: ElementRef;
  filter = {
    fShow: ['All', 'My Questions'],
    fSortBy: ['All', 'Recent']
  };
  isQuestionAskedBySameUser: boolean = false;

  constructor(private modalService: NgbModal, private service: AuthService) { }

  ngOnInit(): void {
    this.editor = new Editor();
    
    this.service.getQuestions().subscribe(
      (res:any) => {
        this.allQuestions = (<Question[]> res).reverse();
        this.filteredQuestions = this.allQuestions;
        this.questions = this.allQuestions;
      },
      err => console.error('Error:', err)
    );

    this.service.getCategories().subscribe(
      (res:any) => {
        this.categories = <Category[]> res;
      },
      err => console.log('Error:', err)
    );

    fromEvent(this.questionSearchInput.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value.toLowerCase().trim();
      }),
      debounceTime(1000),
      distinctUntilChanged()
    ).subscribe((textSearched) => {
      this.questions = this.filteredQuestions.filter(question => question.question.toLowerCase().includes(textSearched) || question.description.toLowerCase().includes(textSearched));
    });
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  toggleEditor() {
    this.editor.destroy();
    this.editor = new Editor();
    this.editorSm = !this.editorSm;
  }

  open(content) {
    this.editorModal = new Editor();
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
      result.description = result.description.content[0].content[0].text;
      let question = <Question> result;
      this.service.submitQuestion(question);
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    this.editorModal.destroy();
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  public ActiveQuestion(id: number) {
    this.activeQuestionId = id;
    this.activeQuestion = this.questions.find((question) => question.id == id);
    this.isQuestionAskedBySameUser = (this.service.userId != null && (this.service.userId == this.activeQuestion.userId)) ? true : false;

    /* increase view by 1 */
    this.service.countView(id);

    /* get solution for this question */
    this.service.getSolutions(id).subscribe(
      (res:any) => {
        this.solutions = (<Solution[]> res).reverse();
      },
      err => console.error('Error:', err)
    );
  }

  public submitSolution() {
    var solution = new Solution();
    solution.answer = this.html.replace(/<\/?[^>]+(>|$)/g, "");
    solution.questionId = this.activeQuestionId;
    solution.userId = this.service.userId;
    this.html = '';
    this.service.submitSolution(solution);
  }

  public countUpvote(questionId: number) {
    this.service.countUpvote(questionId);
  }

  public likeDislike(answerId: Number, liked: boolean) {
    if (this.service.userId) {
      let feedback = new LikeDislike();
      feedback.answerId = answerId;
      feedback.userId = this.service.userId;
      feedback.sentiment = liked;
      this.service.countLikes(feedback);
    }
  }

  filterQuestionList(form: NgForm) {
    console.log('FormData:', form.value);
    if (form.value.filterCategory == 'All' || form.value.filterCategory == null) {
      this.filteredQuestions = this.allQuestions;
    } else {
      this.filteredQuestions = this.allQuestions.filter((question) => question.category == form.value.filterCategory);
    }
    
    if (form.value.show != 'All' && form.value.show != null) {
      if (this.service.userId) {
        this.filteredQuestions = this.filteredQuestions.filter((question) => question.userId == this.service.userId);
      } else {
        console.log('Login to view your questions!');
      }
    }

    if (form.value.sortby != 'All' && form.value.sortby != null) {
      /* do nothing because already in sorted order */
    }

    if (form.value.searchText != null) {
      this.questions = this.filteredQuestions.filter((question) => question.question.toLowerCase().includes(form.value.searchText.toLowerCase()) || question.description.toLowerCase().includes(form.value.searchText.toLowerCase()));
    } else {
      this.questions = this.filteredQuestions;
    }
  }

  resetFilterForm(form: NgForm) {
    this.questions = this.allQuestions;
    form.reset();
  }

  checked(event: Event, answerId: Number) {
    let checkboxes = document.getElementsByName('markAsBest')!;
    checkboxes.forEach((checkbox) => (<HTMLInputElement> checkbox).checked = false);
    (<HTMLInputElement> event.target).checked = true;

    /* mark active question as resolved */
    this.activeQuestion.solved = true;

    let bestSolution = new Solution();
    bestSolution.id = answerId;
    bestSolution.bestSolution = true;
    bestSolution.questionId = this.activeQuestionId;
    this.service.markBest(bestSolution);
  }
}
