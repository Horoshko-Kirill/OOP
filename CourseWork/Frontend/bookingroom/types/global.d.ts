// global.d.ts
declare namespace gapi {
  export function load(apiName: string, callback: () => void): void;

  namespace auth2 {
    export function init(params: {
      client_id: string;
    }): any;
  }

  namespace client {
    function init(args: {
      apiKey: string;
      clientId: string;
      discoveryDocs: string[];
      scope: string;
    }): Promise<void>;

    namespace calendar {
      namespace events {
        function insert(args: {
          calendarId: string;
          resource: any;
        }): Promise<any>;
      }
    }
  }
}
