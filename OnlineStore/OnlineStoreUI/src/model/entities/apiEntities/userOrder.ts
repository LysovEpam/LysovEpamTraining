import { Product } from './product';
import { OrderStatus } from './additional/orderStatus';
import { SystemUserData } from './systemUserData';

export class UserOrder{
    idEntity: number;
    dateOrder:Date;
    address:string;
    products:Product[];
    orderStatus:OrderStatus;
    userSystemData: SystemUserData;

    constructor(idEntity: number, dateOrder:Date, address:string, products:Product[], orderStatus:OrderStatus, userSystemData: SystemUserData){
        this.idEntity = idEntity;
        this.dateOrder = dateOrder;
        this.address = address;
        this.products = products;
        this.orderStatus = orderStatus;
        this.userSystemData = userSystemData;
    }
}

