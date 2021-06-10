import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Certification } from '../home/model';
import { SearchService } from './search.service';
import { AuthenticationService } from '../authentication/authentication.service';
import { Account } from  '../authentication/Account';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  currentAccount: Account;
  constructor(private authenticationService: AuthenticationService,private service:SearchService,private httpClient: HttpClient, 
              private router: Router, private route: ActivatedRoute) { 
                this.authenticationService.currentAccount.subscribe(x => this.currentAccount = x);
              }

  items: Certification[];
  name = '';
  certificates;
  bool = true;

  ngOnInit(): void {
    this.route.queryParams
    .subscribe(params => {
      this.name = params["name"] || null;
    });
    if (this.name == null)
      this.getAllItem(this.bool);
    else this.search(this.name);
    console.log(this.name);
  }
  public onSearch = async (Id: string) => {
    //localStorage.setItem('search', this.namesearch);
    this.router.navigate(['/examuser'], {queryParams: {id: Id}});
  }
  getImageMime(base64: string)
  {
      if (base64.charAt(0)=='/') return 'jpg';
      else if (base64.charAt(0)=='R') return "gif";
      else if(base64.charAt(0)=='i') return 'png';
      else return 'jpeg';
  }
  

    public getAllItem(bool) {
      if (bool == true) {
        this.certificates = this.service.getAllCertificate().subscribe(
          (data) => {
            if (data != null) {
              data.forEach(element => {
                var extension = this.getImageMime(element.image);
                element.base64 = `data:image/${extension};base64,${element.image}`;  
                if(element.image){
                  this.items.push(element.base64); 
                }
                else {
                  this.items.push(element);      

                }
              });
              console.log(this.items);
            }
          }
        );
      }
    }

    public search(name) {
      this.certificates = this.service.getsearchcertificate(name).subscribe(
        (data) => {
          if (data != null) {
            this.items = []
            this.items = data;
            this.bool = false;
          }
          else {
            this.bool = true;
          }
        }
      )
      console.log(this.items);
    }

  public onSearchinsearch = () => {
    this.router.navigate(['/search']);
  }
  
  getImageSource(base64: string): string
  {
    return `data:image/${this.getImageMime(base64)};base64,${base64}`; 
  }
}
