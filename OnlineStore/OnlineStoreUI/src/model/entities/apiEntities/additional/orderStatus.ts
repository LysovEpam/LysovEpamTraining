export class OrderStatus {
    status:OrderStatusEnum;

    constructor(status:OrderStatusEnum){
        this.status = status;
    }
   
    static getStatusPrint(status:OrderStatusEnum){

        if(status == OrderStatusEnum.NewOrder)
            return 'New order';
        if(status == OrderStatusEnum.Processed)
            return 'Processed';
        if(status == OrderStatusEnum.Paid)
            return 'Paid';
        if(status == OrderStatusEnum.WaitingForDelivery)
            return 'Waiting for delivery';
        if(status == OrderStatusEnum.Canceled)
            return 'Canceled';
        if(status == OrderStatusEnum.Fulfilled)
            return 'Fulfilled';
        
        return '';
    }

    static getAllOrderStatus():OrderStatus[]{

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

    static getAllOrderStatusEnum():OrderStatusEnum[]{

        let allStatus: OrderStatusEnum[] = [
            OrderStatusEnum.NewOrder,
            OrderStatusEnum.Processed,
            OrderStatusEnum.Paid,
            OrderStatusEnum.WaitingForDelivery,
            OrderStatusEnum.Canceled,
            OrderStatusEnum.Fulfilled
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