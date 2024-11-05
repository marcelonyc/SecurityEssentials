﻿-- Restore manual test user data after acceptance tests have run
SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([Id], [UserName], [PasswordHash], [PasswordSalt], [HashStrategy], [PasswordLastChangedDateUtc], [CreatedDateUtc], [Enabled], [Approved], [EmailVerified], [Title], [FailedLogonAttemptCount], [FirstName], [LastName], [TelNoHome], [TelNoWork], [TelNoMobile], [Town], [Postcode], [SkypeName], [SecurityQuestionLookupItemId], [SecurityAnswerSalt], [SecurityAnswer], [EmailConfirmationToken], [PasswordResetExpiryDateUtc], [PasswordResetToken], [PasswordExpiryDateUtc], [NewEmailAddress], [NewEmailAddressToken], [NewEmailAddressRequestExpiryDateUtc]) VALUES (1, N'admin@admin.com', N'3OLPQYB9bwcJZyibMk7izWndr44ksK7DkUCo3b/Ysgi8MsxgiGYn7u4XfvXCJypKi2LM0nH3prwy115h3rirHjv1NCNL10E0Zm0IYBAitZ+4NxbjxP+n63o4Iboy669hvw3wPRiP5PuUalZXOxvW09uj9Qlt6tWRCTnCjkNQ3xle+E8RHi8GpHDpDTyHbpVyYy0iLksKUcGMQewoZI4rS609QMRlT1/dF0y081NLMD3MZo1ItAAFQlWIej7TGnN+bdECQwSGDFmflY3mAN7LWO/oVIgqqHhzZiAKZKmekt0zCcUj6aqucUAX6xnJUaqymDZvyj62Z0YKcE4kAZtSmg==', N'9PANyQY9WdydSnrMiPbfHhRIgzpTFJOWvkmenfenz4cwm3MJAoY8XJ5YhhoME+1BGLZNN9UdmucEqQ909Cbf+g8EMOeJ/dGH8DVsD2lGZPMyWpm7OnKPBt8agKfZTLFpQU2i9EDR1PqPVKSxEz9rEIoEioQ7uN1/eJts7rlJGlWt/L99WSzEuyRZ2vvm+Jd1PuEyWQasAe1W1f6iwz0Z9YIS9lgpkUdMlSci4i2gJNKTIiHoyPg2olnMFoVaSyjHkBC+cdNRrX/StoYn/Gq1Mh6rxwF7QyYsvzO4YAaNqatwlIR+sMX2DKCxuQcHZE9kLjULaI5kMmyGYewzbvkhrg==', 0, CAST(N'2017-05-22 14:32:30.590' AS DateTime), CAST(N'2017-05-22 14:32:30.590' AS DateTime), 1, 1, 1, N'Mrs', 0, N'Admin', N'User', NULL, NULL, N'07740101235', NULL, NULL, NULL, 271, N'9XucTvBOjD/GyYsUhLyGPD7/vfCa3U7hdkr2NloDCXptxs9qSN2rewar/0dQPinLUhzjYyx/8Xzstn5YHHlJioQ8cYHIrK7kHpalo57bgsjJEp2hwwo3MtqO8qLyWKtisGyDkejZrtuNj2HOoRV9F8haU398ymBAP6PM/Z8Fmi2+JnwYtTQq88OXo9xSGvKieEiW5KbKipnYvZccEI5/UMzuZMCJesvoqpyP3Zgnnn9G5c9AB+/7GbX+PNkp8m9tWRcp/rWbBZnaFRnI/blXBYhwnpezmh8S/QetSPjOTSrCkR7Th23E938uatmh9sL4tKTF4rR5Grar7sOEHpDu6g==', N'MsIlv+MH5MW7Up8dr4iKhg==', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Users] ([Id], [UserName], [PasswordHash], [PasswordSalt], [HashStrategy], [PasswordLastChangedDateUtc], [CreatedDateUtc], [Enabled], [Approved], [EmailVerified], [Title], [FailedLogonAttemptCount], [FirstName], [LastName], [TelNoHome], [TelNoWork], [TelNoMobile], [Town], [Postcode], [SkypeName], [SecurityQuestionLookupItemId], [SecurityAnswerSalt], [SecurityAnswer], [EmailConfirmationToken], [PasswordResetExpiryDateUtc], [PasswordResetToken], [PasswordExpiryDateUtc], [NewEmailAddress], [NewEmailAddressToken], [NewEmailAddressRequestExpiryDateUtc]) VALUES (2, N'user@user.com', N'KLS+t0CJpd9bsEdpZXAqJSJoNrnYyDkwUKU4c/0bj5VPSIu0VHhMrmWEVyUIRhVfGa3gyGYSKAFLYtJbYIPSVdMHpHYaoxbzz8c9QHYNHZIixy9c+5uRAXN/trfV8wsZ3pcU68Kq+I7aH4ZGSOnY5wgBKNEpde9YMbqQi3FZvY4RvFH3ybbhUDwNITtJIGvdNFJonfosI3PWBCUjsfdX0Zm3dqt6hlLY7RdW4jSCPlzg84Kt5Qj06VJxRG3/cEbOG3O6pNZRo/BktRA6pL1Pig5wcWvYhCIXC4sLk7ofmJZNVuBo0BLAyzLyLecNOQ2NqwcHlx6NqPSYYu3kWOtZIg==', N'2N9Lx0sSQq3ZUBdnbU/Reyj5/gevbwMOwTASSyqYwY8oTUl0XnJUkjHl5I/6x05in77a7+UY3y3ITrBwHFKpZpUJ0hA9qFsQVbDIwVW9LXQ3Uavcgn1BQr8OYYAiJyY2BtTHqy3Zcrey1AFmxa0zbQ0bLW1rErWkLYszxSDJdfdW8GtefCFKviNk18SdgtLO6qj+mA/JEgcAd7/1O9KwKIs2Y9CbxwocHBsaTO9RGiygWlai4w4/EmgnZEe3Ioqb1aX7Rtr8SDEKHOus0ykZrjtg0ju4QSiGrkBwOEsJYQdEKKPLuDsCLk05AANCkYMa442TaXAcmLnkiA980LHWaA==', 0, CAST(N'2017-05-22 14:32:30.620' AS DateTime), CAST(N'2017-05-22 14:32:30.620' AS DateTime), 1, 1, 1, N'Mr', 0, N'Standard', N'User', NULL, NULL, N'07881231234', NULL, NULL, NULL, 271, N'wsI6AFEK+sDF/efbVIWWYkhgz+2d2bbhgnDdOA3d8OI0rQsujRxETpnFvbMHIuakJN3a6C4QMjMSpEsappLTsnmJUFN1CD7usQKdfZDjF/O6nzQtqwWbApckJSXVB2pgUB5ROE5D/nbNMhWM9X/pZb+/wCsxhJ61HJy4CRJKQryJGBsUiLHQN0uXwYFE+0N3F3hatgQTVn2T3Fh9u6G7y116jqiHKPyfCuurJXDa3J4K9SrIqnHSMcKLuVWLX8Qgj40gZeoax6TD6qKOCB76HoYvQQLdMAhIS5HPIW2SbM/l11a58xelrsBSyr5TcrwptrILUjOSfTnUpisFCd3CMw==', N'+xnLEGN85g2abPsXW9mIQg==', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF

SET IDENTITY_INSERT [dbo].[UserRoles] ON 
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF