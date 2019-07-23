import { Component, OnInit } from '@angular/core';
import { Subscription, identity } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ProductCategoryServerService } from 'src/services/product-category-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';

enum typeActionEnum{
  create,
  update,
  undefined
}

@Component({
  selector: 'app-product-category',
  templateUrl: './product-category.component.html',
  styleUrls: ['./product-category.component.css']
})
export class ProductCategoryComponent {

  querySubscription: Subscription;

  productCategory: ProductCategory;
  productCategoryMessage: string;
  buttonSaveText:string;

  typeActionEnum:typeActionEnum;


  constructor(activateRoute: ActivatedRoute, private localStorageService: LocalStorageService, 
        private productCategoryServer: ProductCategoryServerService, private serverErrorsService:ServerErrorsService) { 
    
    this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  }

  processingQueryParam(queryParam: any){

    this.typeActionEnum = typeActionEnum.undefined;
    this.buttonSaveText = '';

    this.productCategory = new ProductCategory();
    this.productCategory.idEntity = 0;
    this.productCategory.categoryName = '';
    this.productCategory.description = '';
 
    var paramEntityAction = queryParam['action'];
    
    if(paramEntityAction!=null && paramEntityAction == 'create')
    {
      this.typeActionEnum = typeActionEnum.create;
      this.buttonSaveText = 'Create new entity';
    }

    if(paramEntityAction!=null && paramEntityAction == 'update')
    {
      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'Update entity';

      var paramEntityIdEntity = queryParam['idEntity'];
      
      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);

        this.productCategoryServer.getById(idEntity).subscribe((data:ProductCategory) =>{
          this.productCategory = data; },
          error => { 
            this.productCategoryMessage = this.serverErrorsService.processError(error);
          } );
      }
      else  {
        this.typeActionEnum = typeActionEnum.undefined;
        this.productCategoryMessage = 'Incorrect request, product category can not be changed';
      }

    }
  }

  productCategorySave(productCategory: ProductCategory){

    let jwt:string = this.localStorageService.getJwt();

    if(this.typeActionEnum == typeActionEnum.create){

      this.productCategoryServer.create(productCategory, jwt).subscribe((data:any) => {
          this.productCategoryMessage = 'Product category successfully created';
          this.productCategory.categoryName = '';
          this.productCategory.description = '';
        },
        error => {
          this.productCategoryMessage = this.serverErrorsService.processError(error); 
          // console.log(error); 
        } 
      );

    }

    if(this.typeActionEnum == typeActionEnum.update){

      this.productCategoryServer.update(productCategory, jwt).subscribe((data:any) => {
          this.productCategoryMessage = 'Product category updated successfully';
      },
      error => {
        this.productCategoryMessage = this.serverErrorsService.processError(error); 
        // console.log(error); 
      }
      
      );
    }

  }


}
