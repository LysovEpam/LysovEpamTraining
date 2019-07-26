
 import { Component, OnInit } from '@angular/core';
 import { LocalStorageService } from 'src/services/localstorage.service';
 import { Subscription } from 'rxjs';
 import { Router, ActivatedRoute } from '@angular/router';
 import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';

 
@Component({
    selector: 'my-app', 
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'] 
})

export class AppComponent implements OnInit { 

   
    loggedUser = false;
    loggedEditor = false;
    loggedAdmin = false;
  
  
    querySubscription: Subscription;


    constructor(private router: Router,activateRoute: ActivatedRoute,private localStorageService:LocalStorageService){
        
        this.querySubscription = activateRoute.queryParams.subscribe( (queryParam: any) => { this.checkUserLogin(); } );
    } 

    ngOnInit(){
       
    }

    checkUserLogin(){
        
        if(this.localStorageService.getAuthorizationWordDate() > new Date(Date.now()))
        {

            let userRoleName: string = this.localStorageService.getUserRole();
            let role: UserRoleEnum = UserRoleEnum[userRoleName];
            let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

           
            if(userRoleEnum == UserRoleEnum.User){
                 this.loggedUser = true;
                 this.loggedEditor = false;
                 this.loggedAdmin = false;
             }
 
             if(userRoleEnum == UserRoleEnum.Editor){
                 this.loggedUser = false;
                 this.loggedEditor = true;
                 this.loggedAdmin = false;
             }
             if(userRoleEnum == UserRoleEnum.Admin){
                 this.loggedUser = false;
                 this.loggedEditor = false;
                 this.loggedAdmin = true;
             }

        }
        else{
            this.localStorageService.logOutUser();
            this.loggedUser = false;
            this.loggedEditor = false;
            this.loggedAdmin = false;
        } 
    }

    logOut(){
        this.localStorageService.logOutUser();
        this.loggedUser = false;
        this.loggedEditor = false;
        this.loggedAdmin = false;

        this.router.navigate(['singin']);


    }

    

    showStore(){
        this.router.navigate(['store/products'], {
            queryParams:{
              'action': 'selectFilter',
              'minCost': 0,
              'maxCost': 0,
              'productSearch': '',
              'productStatus': '',
              'idProductCategory': [],
            
            }
          });
    }

    showBasket(){
        this.router.navigate(['shopping-basket/products'], {
            queryParams:{
              'action': 'showProductToCart'}
          });
    }
}


