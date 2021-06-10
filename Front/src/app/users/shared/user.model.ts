import { DatePipe } from "@angular/common";

export class User {
    id: number;
    firstName: string;
    lastName: string;    
    sex: string;
    doB: Date;
    email: string;
    phone: string;
    country: string;
    image: string;
    accountId: number;
    account: Account;
}
export class Account {
    id: number;
    userName: string;
    password: string;    
    role: string;
    isActive: boolean;
}