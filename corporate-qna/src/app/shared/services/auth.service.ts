import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../constants';
import { UserData } from '../models/user-data';
import { Category } from '../models/category';
import { Question } from '../models/question';
import { Solution } from '../models/solution';
import { LikeDislike } from '../models/like-dislike';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userId: Number;

  constructor(public oidcSecurityService: OidcSecurityService, public http: HttpClient) { }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  callApi() {
    /* const token = this.oidcSecurityService.getToken();

    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
      }),
    }; */

    return this.http.get(Constants.apiRoot + '/secret');
  }

  getToken() {
    return this.oidcSecurityService.getToken();
  }

  loggedIn(): boolean {
    return !!this.oidcSecurityService.getToken();
  }

  verifyUser(userdata: UserData) {
    this.http.post(Constants.apiRoot + '/verifyuser', userdata).subscribe(
      (res:any) => {
        this.userId = Number(res.userId);
      },
      err => console.log('verifyUser Error:', err)
    );
  }

  addNewCategory(category: Category) {
    this.http.post(Constants.apiRoot + '/addcategory', category).subscribe(
      (res:any) => console.log('added'),
      err => console.log('err category:', err)
    );
  }

  getCategories() {
    return this.http.get(Constants.apiRoot + '/categories');
  }

  submitQuestion(question: Question) {
    question.userId = this.userId;
    this.http.post(Constants.apiRoot + '/submitquestion', question).subscribe(
      (res:any) => console.log('submitted'),
      err => console.error('Error:', err)
    );
  }

  getQuestions() {
    return this.http.get(Constants.apiRoot + '/questions');
  }

  submitSolution(solution: Solution) {
    this.http.post(Constants.apiRoot + '/submitsolution', solution).subscribe(
      (res:any) => console.log('sumbitted'),
      err => console.error('Error:', err)
    );
  }

  getSolutions(id: Number) {
    return this.http.get(Constants.apiRoot + '/solutions/' + id);
  }

  countUpvote(questionId: number) {
    if (this.userId) {
      let data = {
        userId: this.userId,
        questionId: questionId
      };
      this.http.post(Constants.apiRoot + '/upvote', data).subscribe(
        (res: any) => console.log('Upvoted'),
        err => console.error('Error:', err)
      );
    }
  }

  countView(questionId: number) {
    if(this.userId) {
      let data = {
        userId: this.userId,
        questionId: questionId
      };
      this.http.post(Constants.apiRoot + '/view', data).subscribe(
        (res: any) => console.log('View Counted'),
        err => console.error('Error:', err)
      );
    }
  }

  countLikes(feedback: LikeDislike) {
    this.http.post(Constants.apiRoot + '/like', feedback).subscribe(
      res => console.log('Res:', res),
      err => console.error('Error:', err)
    );
  }

  markBest(solution: Solution) {
    this.http.post(Constants.apiRoot + '/markbest', solution).subscribe(
      res => console.log('Res:', res),
      err => console.log('Error:', err)
    );
  }

  getUserDetails() {
    return this.http.get(Constants.apiRoot + '/userdetails');
  }

  getUserDetail(id: Number) {
    return this.http.get(Constants.apiRoot + `/userdetail/${id}`);
  }

  getQuestionsAnsweredByUser(id: Number) {
    return this.http.get(Constants.apiRoot + `/questionsansweredbyuser/${id}`);
  }
}
