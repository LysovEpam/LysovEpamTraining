export class OrderStatus {
    status:OrderStatusEnum;

    constructor(status:OrderStatusEnum){
        this.status = status;
    }
   
    getStatusPrint(){

        if(this.status == OrderStatusEnum.NewOrder)
            return 'New order';
        if(this.status == OrderStatusEnum.Processed)
            return 'Processed';
        if(this.status == OrderStatusEnum.Paid)
            return 'Paid';
        if(this.status == OrderStatusEnum.WaitingForDelivery)
            return 'Waiting for delivery';
        if(this.status == OrderStatusEnum.Canceled)
            return 'Canceled';
        if(this.status == OrderStatusEnum.Fulfilled)
            return 'Fulfilled';
        
        return '';
    }

    static getAllProdcutStatus():OrderStatus[]{

        let allStatus: OrderStatus[] = [
            new OrderStatus(OrderStatusEnum.NewOrder),
            new OrderStatus(OrderStatusEnum.Processed),
            new OrderStatus(OrderStatusEnum.Paid),
            new OrderStatus(OrderStatusEnum.WaitingForDelivery),
            new OrderStatus(OrderStatusEnum.Canceled),
            new OrderStatus(OrderStatusEnum.Fulfilled)
        ]

        return allStatus;
    }

}

export enum OrderStatusEnum {
    NewOrder = 10,
	Processed = 11,
	Paid = 12,
	WaitingForDelivery = 13,
	Canceled = 14,
	Fulfilled = 15
}