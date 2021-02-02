import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { UserData } from 'src/app/shared/models/user-data';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: [ ]
})
export class UsersComponent implements OnInit {
  allUsers: UserData[];
  users: UserData[];
  @ViewChild('userSearchInput', { static: true }) userSearchInput: ElementRef;

  constructor(private service: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.service.getUserDetails().subscribe(
      (res: any) => {
        this.allUsers = <UserData[]> res;
        this.users = this.allUsers;
      },
      err => console.error({err})
    );

    fromEvent(this.userSearchInput.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value.toLowerCase().trim();
      }),
      debounceTime(1000),
      distinctUntilChanged()
    ).subscribe((textSearched) => {
      this.users = this.allUsers.filter(user => user.name.toLowerCase().includes(textSearched) || user.username.toLowerCase().includes(textSearched));
    });
  }

  userDetails(id: number) {
    this.router.navigateByUrl(`/qna/user/${id}`);
  }
}
