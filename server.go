package main

import (
        "net/http"
        "database/sql"
        "fmt"
        "github.com/gorilla/mux"
        "github.com/gorilla/handlers"
        _ "github.com/lib/pq"
)

//We set up a server struct with a router attribute initialized as part of the server state
//(that handles routing to different endpoints defined in routes.go and a database object that is initialized as part of the server state.  
type Server struct {
        Router *mux.Router
        DB *sql.DB
}

//Initialize a server by setting up the desired server state.
//Port is the port of the database you wish to use. Note we use postgres here(hard coded).
//User is the username of the database and password is the password. 
//Dbname is the name of the database.
//This allows us to initialize servers that use routes belonging to the server and 
//connect to a any database you want as long as it's a postgres one and set up correctly.
func (s *Server) Initialize(port int, user string, password string, dbname string) {
        psqlInfo := fmt.Sprintf("port=%d user=%s password=%s dbname=%s sslmode=disable", port, user, password, dbname)
        fmt.Print(psqlInfo)
        db, err := sql.Open("postgres", psqlInfo)
        if err != nil {
                fmt.Print(err)
        }

        s.DB = db
        s.Router = mux.NewRouter()
        s.routes()
}

//Run the server with the parameter values  and the router that was initialized as part of the server's state.
//AllowedHeaders is a slice of strings containing the types of headers we want the server to process. This gives us
//maximum flexibility as we can change these on the fly rather than create and initialize new servers every time.
//AllowedMethods is a string slice contianing the names of the HTTP methods that we want to allow our server to process.
//AllowedOrigins is a string slice containing specific origins that CORS whitelists.
//Port is the http port we want the server to listen on. 
func (s *Server) Run(allowedHeaders []string, allowedMethods []string, allowedOrigins []string, port string) {
        headers := handlers.AllowedHeaders(allowedHeaders)
        methods := handlers.AllowedMethods(allowedMethods)
        origins := handlers.AllowedOrigins(allowedOrigins)

        http.ListenAndServe(port, handlers.CORS(headers, methods, origins)(s.Router))
}

