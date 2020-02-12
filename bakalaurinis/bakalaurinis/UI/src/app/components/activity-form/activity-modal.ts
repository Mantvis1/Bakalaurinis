import { NewActivity } from '../../models/new-activity';

export interface ActivityModal {
  activityFormData: NewActivity,
  formTitle: string,
  formConfirmationButtonName : string
}
