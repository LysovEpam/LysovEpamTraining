import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { Router } from '@angular/router';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { ProductCategoryServerService } from 'src/Services/product-category-server.service';
import { ServerErrorsService } from 'src/Services/server-errors.service';
import { ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';

@Component({
  selector: 'app-shopping-basket',
  templateUrl: './shopping-basket.component.html',
  styleUrls: ['./shopping-basket.component.css']
})
export class ShoppingBasketComponent implements OnInit {

  BasketMessage:string;

  constructor(private productCategoryServer: ProductCategoryServerService, 
    private router: Router,
    private localStorageService:LocalStorageService, 
    private serverErrorsService:ServerErrorsService
    ) { }

  ngOnInit() {
    this.router.navigate(['shopping-basket/products'], {
      queryParams:{
        'action': 'showProductToCart'}
    });
  }

  PlaceOrder(){
    this.BasketMessage = 'order!';
  }

}
