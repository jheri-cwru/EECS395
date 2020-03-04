package main

//Initializes routes for the router, allowing for our REST api
func (s *Server) routes() {
        s.Router.HandleFunc("/signup", s.handleSignUp()).Methods("POST")
	s.Router.HandleFunc("/signin", s.handleSignIn()).Methods("POST")
	s.Router.HandleFunc("/list", s.handleAddListEntry()).Methods("POST")
	s.Router.HandleFunc("/list", s.handleUpdateListEntry()).Methods("PATCH")
	s.Router.HandleFunc("/list", s.handleRemoveListEntry()).Methods("DELETE")
	s.Router.Path("/list/").Queries("gt", "{gt}", "lt", "{lt}").HandlerFunc(s.handleFetchListEntry()).Methods("GET")
}

