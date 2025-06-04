import { gapi } from 'gapi-script';

const CLIENT_ID = '355490894211-64gpk57fj2tcv33u48u4abjt6uola4m8.apps.googleusercontent.com';
const API_KEY = 'AIzaSyDzXBxsQ5uTQnE0rCHapocTWRNjrvJl1C0';
const SCOPES = 'https://www.googleapis.com/auth/calendar.readonly';
const DISCOVERY_DOCS = [
  'https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest',
];

export type CalendarEvent = {
  id?: string;
  summary?: string;
  description?: string;
  location?: string;
  start?: {
    dateTime?: string;
    timeZone?: string;
  };
  end?: {
    dateTime?: string;
    timeZone?: string;
  };
};

export function initGoogleClient(onSuccess: () => void): void {
  function start() {
    gapi.client
      .init({
        apiKey: API_KEY,
        clientId: CLIENT_ID,
        discoveryDocs: DISCOVERY_DOCS,
        scope: SCOPES,
      })
      .then(onSuccess)
      .catch((error) => {
        console.error('Google API client init error:', error);
      });
  }

  gapi.load('client:auth2', start);
}

export function signIn(): Promise<gapi.auth2.GoogleUser> {
  const authInstance = gapi.auth2.getAuthInstance();
  if (!authInstance) {
    return Promise.reject(new Error('Google Auth instance not initialized'));
  }
  return authInstance.signIn();
}

export async function listUpcomingEvents(): Promise<CalendarEvent[]> {
  try {
    // @ts-ignore
    const response = await gapi.client.calendar.events.list({
      calendarId: 'primary',
      timeMin: new Date().toISOString(),
      showDeleted: false,
      singleEvents: true,
      orderBy: 'startTime',
      maxResults: 10,
    });

    return (response.result.items ?? []) as CalendarEvent[];
  } catch (error) {
    console.error('Error fetching events:', error);
    return [];
  }
}
