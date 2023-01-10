/* 
  SERVISI SU INJECTABILNI
  SINGLETONE SU TIPA (Zive dok zivi aplikacija) (Razlika izmedju komponente i servisa jeste ta sto komponenta nakon rutiranja se unistava)
*/

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, VirtualTimeScheduler } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService { //Servis zasluzan za vrsenje Http requesta 
  //Servis je dobar zato sto moze posluziti za spremanje nekih podataka u browseru
  baseUrl = 'https://localhost:7026/Login'
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource;
  constructor(private http: HttpClient) 
  {

  }

  login(model: any){
    //REQUEST , TIJELO REQUESTA
    return this.http.post<User>(this.baseUrl, model).pipe(
      map((response:User) => {
        const user = response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    ) 
  }
  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUser$.next(null);
  }
}
