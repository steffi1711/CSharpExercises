import { HotelReview } from './models/hotelreview.model';
import { Hotel } from './models/hotel.model';
import { Headers,Http,Response } from '@angular/http';
import { Injectable } from "@angular/core";


@Injectable()
export class DataService {

    API_Url: string ="http://localhost:5000/api/";

    constructor(private http:Http)
    {

    }

    getHotels(){
        return this.http.get(this.API_Url + "hotel")
        .map((response:Response)=>response.json() as Hotel[]);
    }
    getReviewByHotel(hotelId:string){
        return this.http.get(this.API_Url + "GetByHotelId/"+hotelId)
        .map((response:Response)=>response.json() as HotelReview[]);
    }
}