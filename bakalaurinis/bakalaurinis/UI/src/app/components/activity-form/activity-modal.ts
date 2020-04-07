import { NewActivity } from '../../models/new-activity';

export interface IActivityModal {
  activityFormData: NewActivity,
  formTitle: string,
  formConfirmationButtonName : string;
}
