import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = 'client'; //primjer koriscenja typescripte - Typescript po defaultu prepozna vrijednsoti, ali bolje ih je navoditi
  user: any; //Iskljucivanje typescripte  - kao u javi varijable moze biti bilo kakvog tipa 
  constructor(private http: HttpClient) 
  {
    
  }
  //Kreira se metoda koja ce biti zasluzna za inicijalizaciju  sa implements OnInit u definisanju klase
  ngOnInit(): void {
    this.http.get("https://localhost:7026/User/get-all").subscribe({
      next: /* kazemo sta ce se desit kad nam url vrati podatke */ (response) => this.user = response,
      error: /* kazemo sta ce bit ako pukne */ (error) => {console.log(error)},
      complete: /* na kraju sta ce bit */ () => {console.log('Request has completed')} 
    }) //Observable guglat malo o tome subskripcije i tkao to
  }
  
}
