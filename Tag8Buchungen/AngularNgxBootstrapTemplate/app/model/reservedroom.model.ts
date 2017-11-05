import { Guest } from './guest.model';
import { Room } from './room.model';
export class ReservedRoom{
    room:Room;
    roomId:number;
    from:Date;
    to:Date;
    numOfAdults: number;
    numOfChilds: number;
    guest:Guest;
}