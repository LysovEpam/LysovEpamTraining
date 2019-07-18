import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from 'src/Services/api-settings.service';
import { Router } from '@angular/router';
import { ProductDataRequest } from 'src/model/entities/apiRequests/productDataRequest';
import { ProductSearchRequest } from 'src/model/entities/apiRequests/productSearchRequest';
import { ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  testMessage: string;

  constructor(private router: Router, private localStorageService:LocalStorageService, private http: HttpClient, 
    private apiSettings:ApiSettingsService) { }

  ngOnInit() {
    
    
  }

  productCategoryCreate(){
    this.router.navigate(['/product-category'], {
      queryParams:{
        'action': 'create'} 
    });
  }
  productCategoryUpdate(){
    this.router.navigate(['/product-category'], {
      queryParams:{
        'action': 'update',
        'idEntity': 1}
    });
  }
  productCategoryList(){
    this.router.navigate(['/product-categories']);
  }

  productInformationCreate(){
    this.router.navigate(['/product-information'], {
      queryParams:{
        'action': 'create'} 
    });
  }
  productInformationUpdate(){
    this.router.navigate(['/product-information'], {
      queryParams:{
        'action': 'update',
        'idEntity': 1}
    });
  }
  productInformationList(){
    this.router.navigate(['/product-informations']);
  }

  productCreate(){
    this.router.navigate(['/product'], {
      queryParams:{
        'action': 'create'} 
    });
  }
  productUpdate(){
    this.router.navigate(['/product'], {
      queryParams:{
        'action': 'update',
        'idEntity': 1}
    });
  }
  productShowInformation(){
    this.router.navigate(['/product'], {
      queryParams:{
        'action': 'show',
        'idEntity': 1}
    });
  }

  
  productListChange(){
    this.router.navigate(['/products'], {
      queryParams:{
        'action': 'changeAll'}
    });
  }

  productListSelect(){

   
    this.router.navigate(['/products'], {
      queryParams:{
        'action': 'selectFilter',
        'minCost': 1,
        'maxCost': 1000000,
        'productSearch': '',
        // 'productStatus': [ProductStatusEnum[ProductStatusEnum.NeedToOrder], ProductStatusEnum[ProductStatusEnum.NotAvailable]] ,
        'productStatus': '',
        'idProductCategory': [],
      
      }
    });
  }

  productListCart(){
    this.router.navigate(['/products'], {
      queryParams:{
        'action': 'showProductToCart'}
    });
  }



  

}
