import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CategoriesComponent } from './qna/categories/categories.component';
import { HomeComponent } from './qna/home/home.component';
import { QnaComponent } from './qna/qna.component';
import { UserComponent } from './qna/user/user.component';
import { UsersComponent } from './qna/users/users.component';

const routes: Routes = [
  {path: '', redirectTo: '/qna/home', pathMatch: 'full'},
  {path: 'qna', component: QnaComponent, children: [
    {path: 'home', component: HomeComponent},
    {path: 'categories', component: CategoriesComponent},
    {path: 'users', component: UsersComponent},
    {path: 'user/:id', component: UserComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
