export enum RoleType
{
    Doctor, 
    Cardiolog, 
    Patient
}


export class LoginToken {
    id!: string;
    validUntil!: Date;
    roleType!: RoleType;

    get isExpired(): boolean {
        return new Date() > new Date(this.validUntil);
    }
}
