package main

import (
	"fmt"
	"net/http"
	"strings"
        "encoding/base64"
	"github.com/google/uuid"
)

//Represents an entry in the account_info table
type User struct {
	Email string
	UUID string 
	Hash string 
	PubKey string
}

//Extract the email and the hash and return them
func getAuthenticationInfo(r *http.Request) (string, string) {
        fmt.Print(r.Header.Get("Authorization"))
	decoded, err := base64.StdEncoding.DecodeString(r.Header.Get("Authorization"))

	if err != nil {
		fmt.Print(err)
	}

	authSlice := strings.Split(string(decoded), ":")

        //Return email and hash
        return authSlice[0], authSlice[1]
}
//Processes a POST request to the /signup endpont.
//Request must have headers: 
//Authorization: <email>:<hash> with <email>:<hash> base64 encoded.
//X-Public-Key: <public key>
func (s *Server) handleSignUp() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {
		query := "INSERT INTO account_info(email, uuid, hash, pub_key) VALUES ($1, $2, $3, $4);"
		var newUser User
		newUser.Email, newUser.Hash = getAuthenticationInfo(r)
		newUser.UUID = uuid.New().String()
		newUser.PubKey = r.Header.Get("X-Public-Key")

		_, err := db.Exec(query, newUser.Email, newUser.UUID, newUser.Hash, newUser.PubKey)
	
		fmt.Println("correct")

		if err != nil {
			fmt.Println(err)
			w.WriteHeader(http.StatusBadRequest)
		} else {
			w.WriteHeader(http.StatusOK)
		}
        }
}

func (s *Server) handleSignIn() http.HandlerFunc {
	//db := s.DB
	return func(w http.ResponseWriter, r *http.Request) {
		fmt.Println("Not implemented yet")
	}
}
