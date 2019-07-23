import { Component, OnInit } from '@angular/core';
import { Subscription, identity } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ProductCategoryServerService } from 'src/services/product-category-server.service';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { ProductInformation } from 'src/model/entities/apiEntities/productInformation';
import { DialogData } from 'src/Modules/dialog-modules/confirm-dialog/confirm-dialog';
import { MessageDialog } from 'src/Modules/dialog-modules/message-dialog/message-dialog';
import {MatDialog } from '@angular/material/dialog';
import { ProductInformationServerService } from 'src/services/product-information-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { CategoryDialogData } from 'src/Modules/dialog-modules/category-dialog/category-dialog';
import { CategoryDialog } from 'src/Modules/dialog-modules/category-dialog/category-dialog';
import { Product } from 'src/model/entities/apiEntities/product';
import { ProductServerService } from 'src/services/product-server.service';
import { UserOrder } from 'src/model/entities/apiEntities/userOrder';
import { OrderServerService } from 'src/services/order-server-service';
import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';
import { OrderStatus, OrderStatusEnum } from 'src/model/entities/apiEntities/additional/orderStatus';

enum typeActionEnum{
  show,
  update,
  undefined
}

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  querySubscription: Subscription;
  typeActionEnum:typeActionEnum;
  enableChangeOrder:boolean;

  buttonSaveText:string;
  orderMessage: string;
  //ProductMessage:string;

  allProducts: Product[];
  orderInformation: UserOrder;


  constructor(activateRoute: ActivatedRoute, private localStorageService: LocalStorageService, public dialog: MatDialog,
    private orderServer: OrderServerService, private productServer: ProductServerService,
    private serverErrorsService:ServerErrorsService) { 

    this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  }

  ngOnInit() {

    //this.loadProductCategories();
    this.orderInformation = new UserOrder(0, new Date(Date.now()), '', [], new OrderStatus(OrderStatusEnum.NewOrder), null);
    //this.orderInformation.products = [];

  }

  addProduct(product: Product){

    if(product == null)
    {
      this.orderMessage = 'Need select product';
      //this.ProductMessage = 'Need select product';
      return;
    }
    const index: number = this.orderInformation.products.indexOf(product);
    if (index !== -1) {
      //this.ProductMessage = 'Product has already been added';
    }
    else{
      this.orderInformation.products.push(product)
      //this.ProductMessage = 'Product has been added';
    }
  }

  showProduct(product: Product){


    // let dialogData: CategoryDialogData = new CategoryDialogData(productCategory.categoryName, productCategory.description, productCategory.idEntity);
    // const dialogRef = this.dialog.open(CategoryDialog, {
    //   data: dialogData });

  }

  deleteProduct(product: Product){

    // const index: number = this.productInformation.productCategories.indexOf(productCategory);
    // if (index !== -1) {
    //   this.productInformation.productCategories.splice(index, 1);
    //   this.ProductMessage = 'Category has been deleted';
    // }
  }

  processingQueryParam(queryParam: any){

    this.typeActionEnum = typeActionEnum.undefined;
    this.buttonSaveText = '';

    this.enableChangeOrder = false;
 
    var paramEntityAction = queryParam['action'];
    let userRoleName: string = this.localStorageService.getUserRole();
    let role: UserRoleEnum = UserRoleEnum[userRoleName];
    let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];
       

     if(paramEntityAction!=null && paramEntityAction == 'show')
     {
       this.typeActionEnum = typeActionEnum.show;
       this.buttonSaveText = 'Show entity';
 
       var paramEntityIdEntity = queryParam['idEntity'];
       
       if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
       {
         var idEntity: number = Number(paramEntityIdEntity);
         let jwt: string = this.localStorageService.getJwt();
 
         this.orderServer.getById(idEntity, jwt).subscribe((data: UserOrder) =>{
           this.orderInformation = data; },
           error => { 
             this.orderMessage = this.serverErrorsService.processError(error);
           } );
       }
       else  {
         this.typeActionEnum = typeActionEnum.undefined;
         this.orderMessage = 'Incorrect request, order information can not be showed';
       }
 
     }
    
    else if(paramEntityAction!=null && paramEntityAction == 'update')
    {
      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'Update entity';

      if(userRoleEnum == UserRoleEnum.Admin){
        this.enableChangeOrder = true;
      }

      var paramEntityIdEntity = queryParam['idEntity'];
      
      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);
        let jwt: string = this.localStorageService.getJwt();

        this.orderServer.getById(idEntity, jwt).subscribe((data: UserOrder) =>{
          this.orderInformation = data; },
          error => { 
            this.orderMessage = this.serverErrorsService.processError(error);
          } );
      }
      else  {
        this.typeActionEnum = typeActionEnum.undefined;
        this.orderMessage = 'Incorrect request, order information can not be changed';
      }

    }
  }

  loadProductCategories(){

    this.productServer.getList().subscribe((data: Product[]) =>{
      this.allProducts = data;
    },
      error => { 
        this.orderMessage = this.serverErrorsService.processError(error);
      } );

  }

  productInformationSave(productInformation: ProductInformation){

    this.orderMessage = '';

    let jwt:string = this.localStorageService.getJwt();

    // if(this.typeActionEnum == typeActionEnum.show){

    //   this.productInformationServer.create(productInformation, jwt).subscribe((data:any) => {
    //       this.OrderMessage = 'Product information successfully created';
    //       this.productInformation.productName = '';
    //       this.productInformation.description = '';
    //       this.productInformation.imageLocalSource = '';
    //       this.productInformation.productCategories = [];
    //     },
    //     error => {
    //       this.OrderMessage = this.serverErrorsService.processError(error);
    //     } 
    //   );
    // }

    // if(this.typeActionEnum == typeActionEnum.update){

    //   this.productInformationServer.update(productInformation, jwt).subscribe((data:any) => {
    //       this.OrderMessage = 'Product information updated successfully';
    //     },
    //     error => {
    //       this.OrderMessage = this.serverErrorsService.processError(error);
    //     });
    // }

  }

  



}
