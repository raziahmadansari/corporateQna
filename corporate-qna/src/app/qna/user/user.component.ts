import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { LikeDislike } from 'src/app/shared/models/like-dislike';
import { Question } from 'src/app/shared/models/question';
import { Solution } from 'src/app/shared/models/solution';
import { UserData } from 'src/app/shared/models/user-data';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: [ ]
})
export class UserComponent implements OnInit {
  questions: Question[];
  answeredQuestions: Question[];
  answers: Question;
  solutions: Solution[];
  activeQuestionId: Number = 0;
  activeQuestion: Question;
  userDetail: UserData;
  isQuestionAskedBySameUser: boolean = false;
  @ViewChild('questionsSection', { static: true }) questionsSection: ElementRef;
  @ViewChild('answersSection', { static: true }) answersSection: ElementRef;

  constructor(private route: ActivatedRoute, private service: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((param) => {
      this.service.getUserDetail(param.id).subscribe(
        (res: any) => {
          this.userDetail = <UserData> res;
        },
        err => console.log('Error:', err)
      );

      this.service.getQuestions().subscribe(
        (res:any) => {
          this.questions = (<Question[]> res).reverse().filter((question) => question.userId == param.id);
        },
        err => console.error('Error:', err)
      );

      this.service.getQuestionsAnsweredByUser(param.id).subscribe(
        (res: any) => {
          this.answeredQuestions = <Question[]> res;
        },
        err => console.log('Error:', err)
      );
    });
  }

  activate(event: Event, section: string) {
    let tabs = document.getElementsByName('questionTabs')!;
    tabs.forEach((tab) => (<HTMLBodyElement> tab).classList.remove('active-tab'));
    (<HTMLBodyElement> event.target).classList.add('active-tab');

    if (section == 'questions') {
      (<HTMLBodyElement> this.questionsSection.nativeElement).classList.remove('d-none');
      (<HTMLBodyElement> this.answersSection.nativeElement).classList.add('d-none');
    } else {
      (<HTMLBodyElement> this.questionsSection.nativeElement).classList.add('d-none');
      (<HTMLBodyElement> this.answersSection.nativeElement).classList.remove('d-none');
    }
  }

  public ActiveQuestion(id: number) {
    this.activeQuestionId = id;
    this.activeQuestion = this.questions.find((question) => question.id == id);
    this.isQuestionAskedBySameUser = (this.service.userId != null && (this.service.userId == this.activeQuestion.userId)) ? true : false;

    /* get solution for this question */
    this.service.getSolutions(id).subscribe(
      (res:any) => {
        this.solutions = (<Solution[]> res).reverse();
      },
      err => console.error('Error:', err)
    );
  }

  public ActiveAnsweredQuestion(id: number) {
    this.activeQuestionId = id;
    this.activeQuestion = this.answeredQuestions.find((question) => question.id == id);

    /* get solution for this question */
    this.service.getSolutions(id).subscribe(
      (res:any) => {
        this.solutions = (<Solution[]> res).reverse();
      },
      err => console.error('Error:', err)
    );
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

  checked(event: Event, answerId: Number) {
    let checkboxes = document.getElementsByName('markAsBest')!;
    checkboxes.forEach((checkbox) => (<HTMLInputElement> checkbox).checked = false);
    (<HTMLInputElement> event.target).checked = true;

    /* mark active quesion as resolved */
    this.activeQuestion.solved = true;

    let bestSolution = new Solution();
    bestSolution.id = answerId;
    bestSolution.bestSolution = true;
    bestSolution.questionId = this.activeQuestionId;
    this.service.markBest(bestSolution);
  }

  public countUpvote(questionId: number) {
    this.service.countUpvote(questionId);
  }
}
