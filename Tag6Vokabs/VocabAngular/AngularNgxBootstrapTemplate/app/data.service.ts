import { Wordpairs } from './model/wordpairs.model';
import {Injectable} from "@angular/core"
import {Http, Response} from "@angular/http"
import {Observable} from 'rxjs/Observable';


@Injectable()
export class DataService{
    API_URL: string = "http://localhost:46450/api/"; 

    constructor (private http: Http) { 
        
    } 

    getAllWordPairs() { 
        return this.http.get(this.API_URL+"word") 
        .map((response: Response)=>response.json() as Wordpairs[])
        .catch(err=>{console.log(err);return Observable.of<Wordpairs[]>([])});
    }
    
}