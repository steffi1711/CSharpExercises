import { DataService } from './data.service';
import { Wordpairs } from './model/wordpairs.model';
import {Component} from '@angular/core';

@Component({
  selector: 'my-app',
  templateUrl : 'app/app.component.html',
})
export class AppComponent {
  wordpairs : Wordpairs[] = [];
  counterCorr: number = 0;
  counterError: number = 0;

  constructor(private dataService:DataService){
    this.showWordPairs();
  }

  showWordPairs(){
    this.dataService.getAllWordPairs().subscribe(w =>{
      this.wordpairs = w;
    });
  }

  countCorrect(correct : boolean){
    if(correct ==true)
      this.counterCorr++;
    else  
      this.counterError++;
  }

 
}