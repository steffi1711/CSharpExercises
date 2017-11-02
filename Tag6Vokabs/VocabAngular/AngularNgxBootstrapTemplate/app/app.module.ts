import { Wordpair } from './wordpair.component';
import { DataService } from './data.service';
import { Wordpairs } from './model/wordpairs.model';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { DatepickerModule, AlertModule } from 'ng2-bootstrap';
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";
import 'rxjs/add/observable/of'
import { AppComponent } from './app.component';

import { HttpModule, JsonpModule } from "@angular/http";

@NgModule({
  declarations: [AppComponent, Wordpair],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    JsonpModule,
    AlertModule.forRoot(),
    DatepickerModule.forRoot()
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})

export class AppModule {
}