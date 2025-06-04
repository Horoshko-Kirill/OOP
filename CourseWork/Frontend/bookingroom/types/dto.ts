export interface UserDto {
  id: number;
  name?: string;
  email?: string;
  password?: string;
  role: UserRole;
}

export enum UserRole {
  Admin = 0,
  User = 1,
}

export interface MeetingRoomDto {
  id: number;
  name?: string;
  description?: string;
  capacity: number;
  isActive: boolean;
  workStartTime: string;
  workEndTime: string;
}

export interface EquipmentDto {
  id: number;
  name?: string;
  description?: string;
  meetingRoomId: number;
}

export interface BookingDto {
  id: number;
  title?: string;
  description?: string;
  startTime: string;
  endTime: string;
  createdAt: string;
  organizerId: number;
  meetingRoomId: number;
  organizer?: UserDto;
  meetingRoom?: MeetingRoomDto;
}

export interface InvitationDto {
  id: number;
  status: InvitationStatus;
  sentAt: string;
  respondedAt?: string;
  bookingId: number;
  invitedUserId: number;
  inviterId: number;
  invitedUser?: UserDto;
  inviter?: UserDto;
}

export enum InvitationStatus {
  Pending = 0,
  Accepted = 1,
  Declined = 2,
}

export interface ParticipantDto {
  id: number;
  status: ParticipantStatus;
  createdAt: string;
  bookingId: number;
  userId: number;
  user?: UserDto;
}

export enum ParticipantStatus {
  Pending = 0,
  Accepted = 1,
  Declined = 2,
}
