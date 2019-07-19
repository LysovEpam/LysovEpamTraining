import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { Router } from '@angular/router';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { ProductCategoryServerService } from 'src/Services/product-category-server.service';
import { ServerErrorsService } from 'src/Services/server-errors.service';
import { ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';
import { OrderServerService } from 'src/Services/order-server-service';
import { OrderRequest } from 'src/model/entities/apiRequests/orderRequest';
import { OrderStatus, OrderStatusEnum } from 'src/model/entities/apiEntities/additional/orderStatus';
import { DialogData, ConfirmDialog } from 'src/Modules/dialog-modules/confirm-dialog/confirm-dialog';
import { MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-shopping-basket',
  templateUrl: './shopping-basket.component.html',
  styleUrls: ['./shopping-basket.component.css']
})
export class ShoppingBasketComponent implements OnInit {

  BasketMessage:string;
  address:string;

  constructor(private productCategoryServer: ProductCategoryServerService, 
    private router: Router,public dialog: MatDialog,
    private localStorageService:LocalStorageService, 
    private serverErrorsService:ServerErrorsService,
    private orderServerService: OrderServerService
    ) { }

  ngOnInit() {
    this.router.navigate(['shopping-basket/products'], {
      queryParams:{
        'action': 'showProductToCart'}
    });
  }



  PlaceOrder(){

    var idProducts:number[] = this.localStorageService.getProductCart();

    if(idProducts == null || idProducts.length==0){
      this.BasketMessage = 'You need to choose products to place an order.';
      return;
    }

    if(this.address == null || this.address.length < 5){
      this.BasketMessage = 'You must specify the delivery address( minimum 5 characters)';
      return;
    }

    this.BasketMessage = 'order!';

    let jwt:string = this.localStorageService.getJwt();
    let login:string = this.localStorageService.getUserLogin();
    let status:string = OrderStatusEnum[OrderStatusEnum.NewOrder];

    let orderRequest:OrderRequest = new OrderRequest(0, login, idProducts, this.address, status);


    let dialogData: DialogData = new DialogData('Send an order?', 'confirm sending the order');
     
    const dialogRef = this.dialog.open(ConfirmDialog, { data: dialogData });
  
    dialogRef.afterClosed().subscribe(result => {

      if(result== true)
      {
        //this.BasketMessage = '!!!!!! save order';
        this.saveOrder(orderRequest, jwt);
          

      }
    });

  }

  saveOrder(orderRequest:OrderRequest, jwt:string){

    this.BasketMessage = 'Order successfully created';
    this.localStorageService.deleteAllProductCart();

    this.router.navigate(['shopping-basket'], {
      queryParams:{
        'action': 'showProductToCart'}
    });

    this.orderServerService.create(orderRequest, jwt).subscribe((data:any) => {
      this.BasketMessage = 'Order successfully created';
      this.localStorageService.deleteAllProductCart();
    },
    error => { 
      this.BasketMessage = this.serverErrorsService.processError(error); } 
    );
  }

}
