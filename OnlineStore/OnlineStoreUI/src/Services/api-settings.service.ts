import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiSettingsService {

    private readonly apiUrl: string = 'http://localhost:5000/api';

    //login
    getUrlRegistration():string  {return this.apiUrl + "/registration/RegistrationNewUser";}
    getUrlAuthorization():string  {return this.apiUrl + "/authorization/Authorization";}

    //product catehory
    getUrlProductCategoryGetList():string  {return this.apiUrl + "/ProductCategory/GetList";}
    getUrlProductCategoryGetById():string  {return this.apiUrl + "/ProductCategory/GetById";}
    getUrlProductCategorySearch():string  {return this.apiUrl + "/ProductCategory/Search";}
    getUrlProductCategoryCreate():string  {return this.apiUrl + "/ProductCategory/Create";}
    getUrlProductCategoryUpdate():string  {return this.apiUrl + "/ProductCategory/Update";}
    getUrlProductCategoryDelete():string  {return this.apiUrl + "/ProductCategory/Delete";}

    //product information
    getUrlProductInformationGetList():string  {return this.apiUrl + "/ProductInformation/GetList";}
    getUrlProductInformationGetById():string  {return this.apiUrl + "/ProductInformation/GetById";}
    getUrlProductInformationSearch():string  {return this.apiUrl + "/ProductInformation/Search";}
    getUrlProductInformationCreate():string  {return this.apiUrl + "/ProductInformation/Create";}
    getUrlProductInformationUpdate():string  {return this.apiUrl + "/ProductInformation/Update";}
    getUrlProductInformationDelete():string  {return this.apiUrl + "/ProductInformation/Delete";}

    //product
    getUrlProductGetList():string  {return this.apiUrl + "/Product/GetList";}
    getUrlProductGetById():string  {return this.apiUrl + "/Product/GetById";}
    getUrlProductSearch():string  {return this.apiUrl + "/Product/Search";}
    getUrlProductGetByIdList():string  {return this.apiUrl + "/Product/GetByIdList";}
    getUrlProductCreate():string  {return this.apiUrl + "/Product/Create";}
    getUrlProductUpdate():string  {return this.apiUrl + "/Product/Update";}
    getUrlProductDelete():string  {return this.apiUrl + "/Product/Delete";}

    
}
