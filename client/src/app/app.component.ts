import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './_services/account.service';
import { User } from './_models/User';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = 'client'; //primjer koriscenja typescripte - Typescript po defaultu prepozna vrijednsoti, ali bolje ih je navoditi
  user: any; //Iskljucivanje typescripte  - kao u javi varijable moze biti bilo kakvog tipa 
  constructor(private http: HttpClient, private accountService: AccountService) 
  {
    
  }
  //Kreira se metoda koja ce biti zasluzna za inicijalizaciju  sa implements OnInit u definisanju klase
  ngOnInit(): void {
    this.setCurrentUser();
  }
  setCurrentUser(){
    const user:User = JSON.parse(localStorage.getItem('user')!);
    if(!user) return;
    this.accountService.setCurrentUser(user);
  }
}
