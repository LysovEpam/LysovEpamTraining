import { OnInit, Component } from '@angular/core';
import { ViewChild} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { MatDialog} from '@angular/material/dialog';
import { DialogData, ConfirmDialog } from '../../dialog-modules/confirm-dialog/confirm-dialog';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { Product } from 'src/model/entities/apiEntities/product';
import { ProductServerService } from 'src/services/product-server.service';
import { Subscription, identity } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductSearchRequest } from 'src/model/entities/apiRequests/productSearchRequest';
import { ProductStatus } from 'src/model/entities/apiEntities/additional/productStatus';
import { CategoryDialogData, CategoryDialog } from 'src/Modules/dialog-modules/category-dialog/category-dialog';
import { ProductCartDialog } from 'src/Modules/dialog-modules/product-cart/product-cart';
import { UserRoleEnum, UserRole } from 'src/model/entities/apiEntities/additional/userRole';
import { UserOrder } from 'src/model/entities/apiEntities/userOrder';
import { OrderServerService } from 'src/services/order-server-service';
import { OrderSearchRequest } from 'src/model/entities/apiRequests/orderSearchRequest';
import { OrderStatus } from 'src/model/entities/apiEntities/additional/orderStatus';

enum typeActionEnum{
  changeAll,
  selectFilter,
  showMyOrders,
  undefined
}


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  querySubscription: Subscription;
  typeActionEnum:typeActionEnum;

  enableChangeOrder:boolean;


  ordersMessage:string;

  displayedColumns: string[] = ['idOrder','dateOrder', 
  'address', 'nameClient' , 'products','priceProducts', 'status' ,'actions' ];


  dataSource: MatTableDataSource<UserOrder>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(activateRoute: ActivatedRoute, private router: Router,public dialog: MatDialog, 
    private localStorageService: LocalStorageService, 
    private orderServer: OrderServerService,
    private serverErrorsService: ServerErrorsService) {

      this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  
    }

    ngOnInit() {

      this.dataSource = new MatTableDataSource<UserOrder>();
      this.dataSource.paginator = this.paginator;

    }

    processingQueryParam(queryParam: any){

      this.enableChangeOrder = false;

      this.typeActionEnum = typeActionEnum.undefined;
     
      var paramEntityAction = queryParam['action'];

      let userRoleName: string = this.localStorageService.getUserRole();
      let role: UserRoleEnum = UserRoleEnum[userRoleName];
      let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

      
      
      if(paramEntityAction!=null && paramEntityAction == 'changeAll')
      {

        if(userRoleEnum == UserRoleEnum.Admin){
          this.enableChangeOrder = true;
        }

        this.typeActionEnum = typeActionEnum.changeAll;
        
       
        this.loadAllOrders();
      }

      else if(paramEntityAction!=null && paramEntityAction == 'selectFilter')
      {
        if(userRoleEnum == UserRoleEnum.Admin){
          this.enableChangeOrder = true;
        }

        this.typeActionEnum = typeActionEnum.selectFilter;
        this.enableChangeOrder = true;

        let orderSearch:string = queryParam['search'];
        let orderStatus:string = queryParam['status'];

        let orderRequest:OrderSearchRequest = new OrderSearchRequest(orderStatus, orderSearch);

       

         this.loadSearchOrders(orderRequest);

      }
      else if(paramEntityAction!=null && paramEntityAction == 'showMyOrders'){
        
        this.typeActionEnum = typeActionEnum.showMyOrders;
                        
        this.loadUserOrders();
      }
      else{
        this.loadUserOrders();

        this.typeActionEnum = typeActionEnum.undefined;
        this.enableChangeOrder = false;
        
      }
  
      
    }

    showProduct(product:Product){
      const dialogRef = this.dialog.open(ProductCartDialog, {
        data: product });
    }

    getFinalPrice(order:UserOrder){
      let finalCost:number =0;
      order.products.forEach(product => {
        finalCost+=product.price;
      });

      return finalCost;

    }

    getStatusPrint(status:OrderStatus){
      return OrderStatus.getStatusPrint(status.status);

    }

    

    updateOrder(order:UserOrder){
      this.router.navigate(['/order'], {
        queryParams:{
          'action': 'update',
          'idEntity':order.idEntity
        }
      });
    }

   

    deleteOrder(order:UserOrder){
      let jwt: string = this.localStorageService.getJwt();
      // this.orderServer.delete(order.idEntity, jwt);

      let dialogData: DialogData = new DialogData('Delete order category?', 'Confirm the deletion of the order');
   
      const dialogRef = this.dialog.open(ConfirmDialog, { data: dialogData });
  
      dialogRef.afterClosed().subscribe(result => {
        if(result== true)
        {
          let jwt = this.localStorageService.getJwt();
          this.orderServer.delete(order.idEntity, jwt).subscribe((data:any) =>{
            
            this.ordersMessage = 'Order successfully deleted';
            this.loadAllOrders();
          },
          error => { 
            this.ordersMessage = this.serverErrorsService.processError(error);
          } );
        }
      });




    }
    
  
   
  
    loadAllOrders(){

      let jwt: string = this.localStorageService.getJwt();
  
      this.orderServer.getAll(jwt).subscribe((data:UserOrder[]) =>{
        this.dataSource = new MatTableDataSource<UserOrder>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.ordersMessage = this.serverErrorsService.processError(error);
      });
  
    }

    loadSearchOrders(orderSearchRequest:OrderSearchRequest){

      let jwt: string = this.localStorageService.getJwt();

      this.orderServer.GetBySearch(orderSearchRequest, jwt).subscribe((data:UserOrder[]) =>{
        this.dataSource = new MatTableDataSource<UserOrder>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.ordersMessage = this.serverErrorsService.processError(error);
      });
    }

    loadUserOrders(){

      let jwt: string = this.localStorageService.getJwt();
      
      
      this.orderServer.GetByUser(jwt).subscribe((data:UserOrder[]) =>{
        this.dataSource = new MatTableDataSource<UserOrder>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.ordersMessage = this.serverErrorsService.processError(error);
      });

    }
  

}
