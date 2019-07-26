import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/services/localstorage.service';
import { Router } from '@angular/router';
import { ProductCategoryServerService } from 'src/services/product-category-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { OrderStatus, OrderStatusEnum } from 'src/model/entities/apiEntities/additional/orderStatus';

@Component({
  selector: 'app-orders-filters',
  templateUrl: './orders-filters.component.html',
  styleUrls: ['./orders-filters.component.css']
})

export class OrderFilterComponent implements OnInit {

  ordersMessage:string;
    
  searchString:string;

  allStatus:OrderStatus[];
  selectStatus:OrderStatus;

  constructor(private productCategoryServer: ProductCategoryServerService, 
    private router: Router,
    private localStorageService:LocalStorageService, 
    private serverErrorsService:ServerErrorsService
    ) { }

  ngOnInit() {

    this.allStatus = OrderStatus.getAllOrderStatus();
    this.selectStatus = this.allStatus[0];
    this.searchString = '';
  }

  getOrderStatusPrint(orderStatus:OrderStatus){
    return OrderStatus.getStatusPrint(orderStatus.status);
  }



  loadOutletOrders(orderStatus:OrderStatus, searchString:string){

    var statusName:string = OrderStatusEnum[orderStatus.status].toString();

    this.router.navigate(['orders-filters/orders'], {
      queryParams:{
        'action': 'selectFilter',
        'search': searchString,
        'status': statusName
      }
    });

  }
}
