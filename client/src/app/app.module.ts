import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http' //Za obavljanje HTTP Requesta mora se ovo ukljucit na liniji 16.
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


//Komponente moraju biti ovdje deklarisane 
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
