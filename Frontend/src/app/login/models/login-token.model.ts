export enum RoleType
{
    Doctor = 'Doctor',
    Cardiolog = 'Cardiolog',
    Patient = 'Patient'
}


export class LoginToken {
    id!: string;
    validUntil!: Date;
    roleType!: RoleType;
    userId!: string;

    get isExpired(): boolean {
        return new Date() > new Date(this.validUntil);
    }
}
