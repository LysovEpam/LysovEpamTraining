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

enum typeActionEnum{
  create,
  update,
  undefined
}

@Component({
  selector: 'app-product-information',
  templateUrl: './product-information.component.html',
  styleUrls: ['./product-information.component.css']
})
export class ProductInformationComponent implements OnInit {

  querySubscription: Subscription;
  typeActionEnum:typeActionEnum;

  buttonSaveText:string;
  productInformationMessage: string;
  productCategoriesMessage:string;

  allProductCategories: ProductCategory[];
  productInformation: ProductInformation;


  constructor(activateRoute: ActivatedRoute, private localStorageService: LocalStorageService, public dialog: MatDialog,
    private productInformationServer: ProductInformationServerService, private productCategoryServer: ProductCategoryServerService,
    private serverErrorsService:ServerErrorsService) { 

    this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  }

  ngOnInit() {

    this.loadProductCategories();
    this.productInformation.productCategories = [];

  }

  addCategory(productCategory: ProductCategory){

    if(productCategory == null)
    {
      this.productInformationMessage = 'Need select category';
      this.productCategoriesMessage = 'Need select category';
      return;
    }
    const index: number = this.productInformation.productCategories.indexOf(productCategory);
    if (index !== -1) {
      this.productCategoriesMessage = 'Category has already been added';
    }
    else{
      this.productInformation.productCategories.push(productCategory)
      this.productCategoriesMessage = 'Category has been added';
    }
  }

  showCategory(productCategory: ProductCategory){


    let dialogData: CategoryDialogData = new CategoryDialogData(productCategory.categoryName, productCategory.description, productCategory.idEntity);
    const dialogRef = this.dialog.open(CategoryDialog, {
      data: dialogData });

  }

  deleteCategory(productCategory: ProductCategory){

    const index: number = this.productInformation.productCategories.indexOf(productCategory);
    if (index !== -1) {
      this.productInformation.productCategories.splice(index, 1);
      this.productCategoriesMessage = 'Category has been deleted';
    }
  }

  processingQueryParam(queryParam: any){

    this.typeActionEnum = typeActionEnum.undefined;
    this.buttonSaveText = '';

    this.productInformation = new ProductInformation();
    this.productInformation.idEntity = 0;
    this.productInformation.productName = '';
    this.productInformation.description = '';
    this.productInformation.imageLocalSource = '';
    this.productInformation.productCategories = [];
 
    var paramEntityAction = queryParam['action'];
    
    if(paramEntityAction!=null && paramEntityAction == 'create')
    {
      this.typeActionEnum = typeActionEnum.create;
      this.buttonSaveText = 'Create new entity';
    }
    else if(paramEntityAction!=null && paramEntityAction == 'update')
    {
      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'Update entity';

      var paramEntityIdEntity = queryParam['idEntity'];
      
      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);

        this.productInformationServer.getById(idEntity).subscribe((data: ProductInformation) =>{
          this.productInformation = data; },
          error => { 
            this.productInformationMessage = this.serverErrorsService.processError(error);
          } );
      }
      else  {
        this.typeActionEnum = typeActionEnum.undefined;
        this.productInformationMessage = 'Incorrect request, product information can not be changed';
      }

    }
  }

  loadProductCategories(){

    this.productCategoryServer.getList().subscribe((data: ProductCategory[]) =>{
      this.allProductCategories = data;
    },
      error => { 
        this.productInformationMessage = this.serverErrorsService.processError(error);
      } );

  }

  productInformationSave(productInformation: ProductInformation){

    this.productInformationMessage = '';

    let jwt:string = this.localStorageService.getJwt();

    if(this.typeActionEnum == typeActionEnum.create){

      this.productInformationServer.create(productInformation, jwt).subscribe((data:any) => {
          this.productInformationMessage = 'Product information successfully created';
          this.productInformation.productName = '';
          this.productInformation.description = '';
          this.productInformation.imageLocalSource = '';
          this.productInformation.productCategories = [];
        },
        error => {
          this.productInformationMessage = this.serverErrorsService.processError(error);
        } 
      );
    }

    if(this.typeActionEnum == typeActionEnum.update){

      this.productInformationServer.update(productInformation, jwt).subscribe((data:any) => {
          this.productInformationMessage = 'Product information updated successfully';
        },
        error => {
          this.productInformationMessage = this.serverErrorsService.processError(error);
        });
    }

  }

  



}
