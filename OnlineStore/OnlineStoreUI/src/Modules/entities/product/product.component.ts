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
import { ProductServerService } from 'src/services/product-server.service';
import { Product } from 'src/model/entities/apiEntities/product';
import { ProductStatus, ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';
import { ProductDataRequest } from 'src/model/entities/apiRequests/productDataRequest';
import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';

enum typeActionEnum{
  create,
  update,
  show,
  undefined
}

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit {

  querySubscription: Subscription;

  typeActionEnum:typeActionEnum;
  enableChangeProduct:boolean;
  buttonSaveText:string;
  productMessage: string;

  product: Product;
  
  allProductInformations: ProductInformation[];
  allProductStatus: ProductStatus[];
  

  constructor(activateRoute: ActivatedRoute, private localStorageService: LocalStorageService, public dialog: MatDialog,
    private productInformationServer: ProductInformationServerService, 
    private productServer: ProductServerService,
    private serverErrorsService:ServerErrorsService) { 

    this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  }

  ngOnInit() {

    



    this.allProductStatus = ProductStatus.getAllProdcutStatus();
    this.loadProductInformations();

  }

  processingQueryParam(queryParam: any){

    this.typeActionEnum = typeActionEnum.undefined;
    this.buttonSaveText = '';

    this.product = new Product();
    
    var paramEntityAction = queryParam['action'];
    
    if(paramEntityAction!=null && paramEntityAction == 'create')
    {
      this.typeActionEnum = typeActionEnum.create;
      this.buttonSaveText = 'Create new entity';
      this.enableChangeProduct = true;
    }

    if(paramEntityAction!=null && paramEntityAction == 'update'){
      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'Update entity';
      this.enableChangeProduct = true;

      var paramEntityIdEntity = queryParam['idEntity'];
      
      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);

        this.productServer.getById(idEntity).subscribe((data: Product) =>{
          this.product = data; },
          error => { 
            this.productMessage = this.serverErrorsService.processError(error);
          } );
      }
      else  
      {
        this.typeActionEnum = typeActionEnum.undefined;
        this.productMessage = 'Incorrect request, product information can not be changed';
        this.enableChangeProduct = false;
      }

    }

    if(paramEntityAction!=null && paramEntityAction == 'show'){

      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'show entity';
      this.enableChangeProduct = false;

      var paramEntityIdEntity = queryParam['idEntity'];
      
      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);

        this.productServer.getById(idEntity).subscribe((data: Product) =>{
          this.product = data; },
          error => { 
            this.productMessage = this.serverErrorsService.processError(error);
          } );
      }
      else  
      {
        this.typeActionEnum = typeActionEnum.undefined;
        this.productMessage = 'Incorrect request, product information can not be changed';
        this.enableChangeProduct = false;
      }

    }

  }
   
  getCategoryNamesString(productInformation:ProductInformation){

    let result:string = 'Product categories: ';

    productInformation.productCategories.forEach(productCategory => {
      result =result+  productCategory.categoryName + ";  ";
    });

    return result;
  }

 

  loadProductInformations(){

    this.productInformationServer.getList().subscribe((data:ProductInformation[]) =>{
      this.allProductInformations = data;
    },
    error => { 
      this.productMessage = this.serverErrorsService.processError(error);
    });

  }

  productSave(){

    if(((this.product.price == null) || isNaN(Number(this.product.price))))
    {
      this.productMessage = 'Need to specify the correct price';
      return;
    }
    if(this.product.productStatus == null)
    {
      this.productMessage = 'Need to select status';
      return;
    }
    if(this.product.productInformation == null || this.product.productInformation.idEntity==0)
    {
      this.productMessage = 'Need to select product information';
      return;
    }

    let idEntity:number = this.product.idEntity;
    let price:number = this.product.price;
    let statusName: string = ProductStatusEnum[this.product.productStatus.status];
    let idProductInformation: number = Number(this.product.productInformation.idEntity);


    let productRequest: ProductDataRequest = new ProductDataRequest(idEntity, price, statusName, idProductInformation);


     let jwt:string = this.localStorageService.getJwt();

    this.product.idProductInformation = this.product.productInformation.idEntity;
    
    if(this.typeActionEnum == typeActionEnum.create){

      this.productServer.create(productRequest, jwt).subscribe((data:any) => {

          this.productMessage = 'Product successfully created';
          this.product = new Product();
        },
        error => {
          this.productMessage = this.serverErrorsService.processError(error);
        } 
      );
    }

    if(this.typeActionEnum == typeActionEnum.update){

      this.productServer.update(productRequest, jwt).subscribe((data:any) => {
          this.productMessage = 'Product updated successfully';
        },
        error => {
          this.productMessage = this.serverErrorsService.processError(error);
        });
    }

  }

}
