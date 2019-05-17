import { User } from './user';

export class Payment {
  constructor(
    public paymentId?: number,
    public amount?: number,
    public balance?: number,
    public date?: Date,
    public correspondentUser?: User,
    public user?: User) { }
}
