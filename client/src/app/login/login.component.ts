import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model:any={}
  currentUser$: Observable<User | null> = of(null);
  constructor(private accountService: AccountService) {}

  ngOnInit(): void{
    this.currentUser$ = this.accountService.currentUser$;
  }
  getCurrentUser(){
    this.accountService.currentUser$.subscribe({
      error:error=>console.log(error)
    })
  }
  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    });
  }
  logout(){
    this.accountService.logout();
  }
}
