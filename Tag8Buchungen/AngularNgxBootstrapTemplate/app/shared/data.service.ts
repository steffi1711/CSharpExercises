import { ReservedRoom } from './../model/reservedroom.model';
import { Headers,Http,Response } from '@angular/http';
import { Injectable } from "@angular/core";
import { Room } from "../model/room.model";

@Injectable()
export class DataService {

    API_Url: string ="http://localhost:5000/api/";

    constructor(private http:Http)
    {

    }

    getRooms(){
        return this.http.get(this.API_Url + "hotel")
        .map((response:Response)=>response.json() as Room[]);
    }
    getResRoomsByRooms(roomId:string){
        return this.http.get(this.API_Url + "hotel/getbyroom/"+roomId)
        .map((response:Response)=>response.json() as ReservedRoom[]);
    }
}