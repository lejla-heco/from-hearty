export class LoginToken {
    id!: string;
    validUntil!: Date;

    get isExpired(): boolean {
        return new Date() > new Date(this.validUntil);
    }
}
