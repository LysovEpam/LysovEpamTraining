import { OnInit, Component } from '@angular/core';
import { ViewChild} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import { MatDialog} from '@angular/material/dialog';
import { DialogData, ConfirmDialog } from '../../dialog-modules/confirm-dialog/confirm-dialog';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';
import { UserServerService } from 'src/services/user-server.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
}) 
export class UsersComponent implements OnInit {


  usersMessage:string;

  displayedColumns: string[] = ['idUser', 
  'login', 'status', 'role', 'firstName', 'lastName', 'email', 'phone', 'actions'];
  
  dataSource: MatTableDataSource<SystemUserData>;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private router: Router,public dialog: MatDialog, 
    private localStorageService: LocalStorageService, 
    private usersServer: UserServerService,
    private serverErrorsService: ServerErrorsService) {

      
  
    }

    ngOnInit() {

    this.loadAllUsers();

    }
    
    applyFilter(filterValue: string) {
      this.dataSource.filter = filterValue.trim().toLowerCase();
    } 
    
    createUser(){
      
      this.router.navigate(['/user'], {
        queryParams:{
          'action': 'create'}
      });
  
    }
  
    updateUser(user:SystemUserData){
      
      this.router.navigate(['/user'], {
        queryParams:{
          'action': 'update',
          'idEntity': user.idUser}
      });
    }

    
  
    deleteUser(user:SystemUserData){

      
       let dialogData: DialogData = new DialogData('Delete user?', 'Confirm the deletion of the user');
     
       const dialogRef = this.dialog.open(ConfirmDialog, { data: dialogData });
  
      dialogRef.afterClosed().subscribe(result => {
        if(result== true)
        {
         
          let jwt = this.localStorageService.getJwt();
          this.usersServer.delete(user.idUser, jwt).subscribe((data:any) =>{
            
            this.usersMessage = 'User successfully deleted';
            this.loadAllUsers();
          },
          error => { 
            this.usersMessage = this.serverErrorsService.processError(error);
          } );
        }
      } );
  
    }

   
  
    loadAllUsers(){

      let jwt = this.localStorageService.getJwt();
  
      this.usersServer.getAll(jwt).subscribe((data:SystemUserData[]) =>{
        this.dataSource = new MatTableDataSource<SystemUserData>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.usersMessage = this.serverErrorsService.processError(error);
      });
  
    }

   

}
