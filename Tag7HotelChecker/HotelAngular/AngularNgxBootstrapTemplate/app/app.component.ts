import { Hotel } from './models/hotel.model';
import { HotelReview } from './models/hotelreview.model';
import { DataService } from './data.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'my-app',
  templateUrl : 'app/app.component.html',
})
export class AppComponent implements OnInit{
  constructor(private dataService:DataService)
    {
      
    }

    hotels: Hotel[];
    reviews: HotelReview[];
    selectedHotel: Hotel;
   
    ngOnInit(){
        this.getAllHotels();
    }
    
    getAllHotels(){
      this.dataService.getHotels().subscribe(data=> {
        this.hotels = data;
      });
    }

    refreshReviews(hotelId:number){
      this.dataService.getReviewByHotel(hotelId).subscribe(data=>{
        this.reviews = data;
      });
    }

    selectHotel(hotel: Hotel) {
    this.selectedHotel = hotel;
    this.refreshReviews(hotel.id);

  }

}