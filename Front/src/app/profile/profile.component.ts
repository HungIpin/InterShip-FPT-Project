import { Component, OnInit } from '@angular/core';
import { UsersService } from '../users/shared/user.service';
import { User } from '../users/shared/user.model';
import { DialogNewUserComponent } from '../users/dialog-new-user/dialog-new-user.component';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticationService} from '../authentication/authentication.service'


@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})

export class ProfileComponent implements OnInit {

    user: User
    //nap du lieu cho nay
    settings: Object
    //Two way binding
    public currentAcc: number = Number(this.authenticationService.currentAccountValue.id);
    highestScore: number = 0;
    takenTimes: number = 0;
    isEdit: boolean;
    dialogUser: User;
    isLoaded: boolean = false;
    constructor(private service: UsersService, public dialog: MatDialog, private authenticationService: AuthenticationService) {
    }
    
    async ngOnInit()
    {
      //this.user.image = '';
      this.user = await this.getuser();
      this.highestScore = Number(await this.service.gethighestScore(this.currentAcc));
      this.takenTimes = Number(await this.service.gettakenTimes(this.currentAcc));
      this.isLoaded = true;
    }
  
    openEditDialog(): void {
        const dialogRef = this.dialog.open(DialogNewUserComponent, {
          width: '350px',
          data: this.dialogUser
        });
    
        dialogRef.afterClosed().subscribe(result => {
          if (result.id != null)
          {
            console.log('The dialog was closed');
            console.log(result); 
            this.updateUser(result);
          }
        });
      }

    public getuser = async () => {
        const user = await this.service.getuser(this.currentAcc) as User;
        return user;
    }


    pushData(): void {
        this.dialogUser = {
        id: this.user.id,
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        sex: this.user.sex,
        doB: this.user.doB,
        email: this.user.email,
        phone: this.user.phone,
        country: this.user.country,
        accountId: this.user.accountId,
        account: this.user.account,
        image: this.user.image
        }
    }
      
    // private reload = async () => {
    //     this.user = await this.getuser();
    //     this.pushData();
    //     console.log(this.user);
    //   }
    
  
    public updateUser = async (user) => {
      try 
      {
        const result = await this.service.updateusers(user.id, user) as User;
        if (result) 
        {
          alert('Update sucessfully');
          this.user = result;
        }
      }
      catch (e) {
        console.log(e);
      }
    }
    getImageMime(base64: string): string
    {
      if (base64.charAt(0)=='/') return 'jpg';
      else if (base64.charAt(0)=='R') return "gif";
      else if(base64.charAt(0)=='i') return 'png';
      else return 'jpeg';
    }
    getImageSource(): string
    {
      return `data:image/${this.getImageMime(this.user.image)};base64,${this.user.image}`; 
    }

}
