import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/services/localstorage.service';
import { Router } from '@angular/router';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { ProductCategoryServerService } from 'src/services/product-category-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent implements OnInit {

  storeMessage:string;
  // productCategoriesMessage:string;

  priceMin:number;
  priceMax:number;
  searchString:string;

  selectStatus:string;
  allStatus:string[];
  allProductCategories:ProductCategory[];
  selectCategories:ProductCategory[];

  constructor(private productCategoryServer: ProductCategoryServerService, 
    private router: Router,
    private localStorageService:LocalStorageService, 
    private serverErrorsService:ServerErrorsService
    ) { }

  ngOnInit() {

    this.allStatus = ['Available', 'Available, and need to order', 'All status'];

    this.productCategoryServer.getList().subscribe((data:ProductCategory[]) =>{
            this.allProductCategories = data;
        },
        error => { 
            this.storeMessage = this.serverErrorsService.processError(error);
    });


    this.priceMin = 0;
    this.priceMax = 0;

    this.selectStatus = this.allStatus[1];
    this.selectCategories = [];

    this.loadOutletProducts();
  }


  addCategory(productCategory: ProductCategory){

    this.storeMessage = '';

    if(productCategory == null)
    {
      this.storeMessage = 'Need select category';
      return;
    }
    const index: number = this.selectCategories.indexOf(productCategory);
    if (index !== -1) {
       this.storeMessage = 'Category has already been added';
    }
    else{
      this.selectCategories.push(productCategory)
      this.storeMessage = 'Category has been added';
    }
  }

  deleteCategory(productCategory: ProductCategory){

    const index: number = this.selectCategories.indexOf(productCategory);
    if (index !== -1) {
      this.selectCategories.splice(index, 1);
       this.storeMessage = 'Category has been deleted';
    }
  }


  loadOutletProducts(){

    let statuses: string[] = [];
    let idProductCategory:number[] = [];

    if(this.selectStatus == this.allStatus[0]){
      statuses = [ProductStatusEnum[ProductStatusEnum.Available]];
    }
    if(this.selectStatus == this.allStatus[1]){
      statuses = [ProductStatusEnum[ProductStatusEnum.Available], 
      ProductStatusEnum[ProductStatusEnum.NeedToOrder]];
    }
    if(this.selectStatus == this.allStatus[2]){
      statuses = [ProductStatusEnum[ProductStatusEnum.Available], 
      ProductStatusEnum[ProductStatusEnum.NeedToOrder], 
      ProductStatusEnum[ProductStatusEnum.NotAvailable]];
    }
      this.selectCategories.forEach(element => {
        idProductCategory.push(element.idEntity);
    });

    this.router.navigate(['store/products'], {
      queryParams:{
        'action': 'selectFilter',
        'minCost': this.priceMin,
        'maxCost': this.priceMax,
        'productSearch': this.searchString,
        'productStatus': statuses,
        'idProductCategory': idProductCategory,
      
      }
    });
  }
}
