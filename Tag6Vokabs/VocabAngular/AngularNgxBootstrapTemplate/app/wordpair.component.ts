import { Wordpairs } from './model/wordpairs.model';
import {Component, Input, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'word-pair',
  templateUrl : 'app/wordpair.component.html',
})

export class Wordpair{
    _wordpair : Wordpairs;
    count : number = 0;
    inputValue : string = "";
    wordIsCorrect:boolean = false;
    actualWords : string[] = [];

    @Output() correct = new EventEmitter<boolean>();

    @Input()
    set wordpair(value : Wordpairs){
        this._wordpair = value;
        this.actualWords = this._wordpair.rightWord.split(" ");
    }
    get wordpair(){
        return this._wordpair;
    }

    constructor(){
        
    }

    check(){
        if(this.actualWords.indexOf(this.inputValue) >= 0 || this._wordpair.rightWord.indexOf(this.inputValue) == 0){
            this.wordIsCorrect = true;
            this.correct.emit(true);
        }
        else
            this.count++;
            this.correct.emit(false);
    }
}