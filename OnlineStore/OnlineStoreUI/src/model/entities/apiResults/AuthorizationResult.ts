import { UserRole } from '../apiEntities/additional/userRole';

export class AuthorizationResult{
    userRole: UserRole;
    userLogin: string;
    jwt: string;
    dateTimeAuthorizationFinish: Date;
}