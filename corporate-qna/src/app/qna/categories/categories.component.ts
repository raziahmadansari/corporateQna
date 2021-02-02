import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

import { Category } from 'src/app/shared/models/category';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: [ ]
})
export class CategoriesComponent implements OnInit {
  categories: Category[];
  allCategories: Category[];
  filteredCategories: Category[];
  closeResult = '';
  @ViewChild('categorySearchInput', { static: true }) categroySearchInput: ElementRef;

  constructor(private modalService: NgbModal, private service: AuthService) { }

  ngOnInit(): void {
    this.service.getCategories().subscribe(
      (res:any) => {
        this.allCategories = (<Category[]> res).reverse();
        this.categories = this.allCategories;
      },
      err => console.log('Error:', err)
    );

    fromEvent(this.categroySearchInput.nativeElement, 'keyup').pipe(
      map((event:any) => event.target.value.toLowerCase().trim()),
      debounceTime(1000),
      distinctUntilChanged()
    ).subscribe((textSearched) => {
      this.categories = this.allCategories.filter(category => category.name.toLowerCase().includes(textSearched) || category.description.toLowerCase().includes(textSearched));
    });
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
      console.log('new category:', result);
      this.service.addNewCategory(<Category> result);
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  filterCategoryList(filterCategory: NgForm) {
    this.filteredCategories = this.allCategories.slice();
    if (filterCategory.value.category == 'All' || filterCategory.value.category == null) {
      /* do nothing */
    } else if (filterCategory.value.category == 'Popular') {
      this.filteredCategories = this.filteredCategories.sort((a, b) => (a.thisWeek > b.thisWeek ? -1 : 1));
    } else {
      this.filteredCategories = this.filteredCategories.filter(category => category.id == filterCategory.value.category);
    }

    if (filterCategory.value.searchText != null) {
      let textSearched = filterCategory.value.searchText.toLowerCase().trim();
      this.categories = this.filteredCategories.filter(category => category.name.toLowerCase().includes(textSearched) || category.description.toLowerCase().includes(textSearched))
    } else {
      this.categories = this.filteredCategories;
    }
  }

  resetFilterForm(form: NgForm) {
    this.categories = this.allCategories;
    form.reset();
  }
}
