export interface DialogData{
    id: number;
    name: string;
    createdDate : Date;
    description : string;
    accountId :number;
    parentPoolId :number; //dropdown parentpoolid chính là list id của questionpool, bảng tự tham chiếu chính nó cho xin csdl thử
}
