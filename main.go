package main

//Main entry point of the application. 

func main() {

	//Parameters for the initialize method of the server.
	//Change these to change the initial state of the server: what database to use, what user, etc.
	dbPort := 5433
	dbUser := "postgres"
	dbPass := "postgres"
	dbName := "srproj"

	//Paramters for run method of the created server. 
	//Change these to configure CORS/how what the server should accept.
	allowedHeaders := []string{"Authorization"}
	allowedMethods := []string{"POST", "PATCH", "DELETE", "GET"}
	allowedOrigins := []string{"*"}
	httpPort := ":8080"

	//Create new server, configure it, and run it
        s := Server{}
        s.Initialize(dbPort, dbUser, dbPass, dbName)
        s.Run(allowedHeaders, allowedMethods, allowedOrigins, httpPort)
}

