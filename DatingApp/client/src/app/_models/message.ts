export interface Message {
    id: number
    senderId: number
    senderUsername: string
    senderPhotoUrl: string
    recipientId: number
    recipientUser: string
    recipientUsername: string
    content: string
    dateRead: Date
    messageSent: Date
  }