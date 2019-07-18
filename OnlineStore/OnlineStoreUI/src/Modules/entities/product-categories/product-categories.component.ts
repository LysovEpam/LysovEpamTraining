import { OnInit, Component } from '@angular/core';
import { ViewChild} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ConfirmDialog, DialogData } from '../../dialog-modules/confirm-dialog/confirm-dialog';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { ProductCategoryServerService } from 'src/Services/product-category-server.service';
import { ServerErrorsService } from 'src/Services/server-errors.service';


@Component({
  selector: 'app-product-categories',
   templateUrl: './product-categories.component.html',
   styleUrls: ['./product-categories.component.css']
})


export class ProductCategoriesComponent implements OnInit {

  productCategoriesMessage:string;

  displayedColumns: string[] = ['idEntity', 'categoryName', 'description', 'actions'];
  
  dataSource: MatTableDataSource<ProductCategory>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;



  constructor(private router: Router,public dialog: MatDialog, 
    private localStorageService: LocalStorageService, 
    private productCategoryServer: ProductCategoryServerService, private serverErrorsService:ServerErrorsService) {}

  ngOnInit() {

    this.loadProductCategories();
    this.dataSource = new MatTableDataSource<ProductCategory>();
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  } 

  createProductCategory(){
    
    this.router.navigate(['/product-category'], {
      queryParams:{
        'action': 'create'}
    });

  }

  updateProductCategory(product:ProductCategory){
    
    this.router.navigate(['/product-category'], {
      queryParams:{
        'action': 'update',
        'idEntity': product.idEntity}
    });
  }

  deleteProductCategory(product:ProductCategory){
    
    let dialogData: DialogData = new DialogData('Delete product category?', 'Confirm the deletion of the product category');
   
    const dialogRef = this.dialog.open(ConfirmDialog, {
      // width: '350px',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result== true)
      {
        let jwt = this.localStorageService.getJwt();
        this.productCategoryServer.delete(product.idEntity, jwt).subscribe((data:any) =>{
          
          this.productCategoriesMessage = 'Product category successfully deleted';
          this.loadProductCategories();
        },
        error => { 
          this.productCategoriesMessage = this.serverErrorsService.processError(error);
        } );
      }
    });

  }

  loadProductCategories(){

    this.productCategoryServer.getList().subscribe((data:ProductCategory[]) =>{
      
      this.dataSource = new MatTableDataSource<ProductCategory>(data);
      this.dataSource.paginator = this.paginator;
    },
    error => { 
      this.productCategoriesMessage = this.serverErrorsService.processError(error);
    });

  }


  

  

}







