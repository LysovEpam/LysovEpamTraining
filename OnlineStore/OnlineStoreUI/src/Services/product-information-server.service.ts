import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { ProductInformation } from 'src/model/entities/apiEntities/productInformation';

@Injectable({
  providedIn: 'root'
})
export class ProductInformationServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    getList(){
      return this.http.get(this.apiSettings.getUrlProductInformationGetList()); 
    }

    getById(id:number){
      return this.http.get(this.apiSettings.getUrlProductInformationGetById()+'/' + id); 
    }

    search(search:string){
      return this.http.get(this.apiSettings.getUrlProductInformationSearch()+'/' +search); 
    }

    create(productInformation: ProductInformation, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlProductInformationCreate(), productInformation, httpOptions);

    }

    update(productInformation: ProductInformation, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlProductInformationUpdate(), productInformation, httpOptions); 
    }
    
    delete(id: number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.delete(this.apiSettings.getUrlProductInformationDelete()+'/' + id, httpOptions); 
    }

}
