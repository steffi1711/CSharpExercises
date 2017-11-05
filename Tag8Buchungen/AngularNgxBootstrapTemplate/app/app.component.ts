import { ReservedRoom } from './model/reservedroom.model';
import { Room } from './model/room.model';
import { DataService } from './shared/data.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'my-app',
  templateUrl : 'app/app.component.html',
})
export class AppComponent implements OnInit{
   constructor(private dataService:DataService)
    {
      
    }

    rooms: Room[];
    resRooms: ReservedRoom[];
    selectedRoom: Room;
    totalGuests: number = 0;
    resCount: number;
    ngOnInit(){
        this.getAllRooms();
    }
    
    getAllRooms(){
      this.dataService.getRooms().subscribe(data=> {
        this.rooms = data;
      });
    }

    refreshRooms(roomId:string){
      this.dataService.getResRoomsByRooms(roomId).subscribe(data=>{
        this.resRooms = data;
        this.countReservations(roomId)
      });
    }

    selectRoom(room: Room) {
    this.selectedRoom = room;
    this.refreshRooms(room.roomNumber);
    this.totalGuests = 0;
  }

  countReservations(roomId:string)
  {
    for (let resRoom of this.resRooms) {
      this.totalGuests++;
    }
  }

}