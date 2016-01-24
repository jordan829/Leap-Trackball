using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class readgml : MonoBehaviour {

	public struct EDGE {
		public int target, source;
	}

	public struct NODE {
		public int id;
		public string label;
		public int x_pos, y_pos, z_pos;
		public int temp_x, temp_y, temp_z;
		public int force;
		public int offset_x, offset_y, offset_z;
	}

	public struct GRAPH {
		public int n_nodes;
		public int n_edges;
		public List<NODE> nodes;
		public List<EDGE> edges;
	}

	public GRAPH graph;

	// Use this for initialization
	void Start () {
		//fill_buffer
		//create_network
		//get_degrees
		//red_edges
		//free_buffer
		graph.n_nodes = 0;
		graph.n_edges = 0;
		read_file("dolphins.gml");
		int result = printGraph();
		print("Graph result = " + result);
	}

	public int read_file(string filename)
	{
		string line;

		System.IO.StreamReader file = 
			new System.IO.StreamReader(filename);

		bool newNode = false;
		int lastID = -1;
		int lastSource = -1;
		bool newEdge = false;
		while(((line = file.ReadLine()) != null))
		{
			print ("meow");
			int i = 0;
			char[] delims = {' '};
			string[] words = line.Split(delims);
			while( i < words.Length ) {
				if( words[i].CompareTo("") == 0 ) {
					i++;
					break;
				}
				if( words[i].CompareTo("id") == 0 ) {
					newNode = true;

					// add new node to graph, set id as words[i+1]
					lastID = Convert.ToInt32( words[i+1] );
					break;
				}
				if( words[i].CompareTo("label") == 0 ) {
					if(newNode) {
						// get the last node created, add a label
						// ignore quotations
						NODE n = new NODE();
						n.id = lastID;
						n.label = words[i+1];
						graph.nodes.Add(n);
						graph.n_nodes++;

						newNode = false;
						lastID = -1;
						break;
					}
					else {
						// unknown id read, no label accompanying
						break;
					}
				}
				if( words[i].CompareTo("source") == 0 ) {
					newEdge = true;

					// add new edge from indexes
					lastSource = Convert.ToInt32( words[i+1] );
					break;
				}
				if( words[i].CompareTo("target") == 0) {
					if(newEdge) {
						// get last edge value created
						EDGE e = new EDGE();
						e.source = lastSource;
						e.target = Convert.ToInt32( words[i+1] );
						graph.edges[graph.n_edges] = e;
						graph.n_edges++;

						newEdge = false;
						lastSource = -1;
						break;
					}
					else {
						// failure
						break;
					}
				}
			}
		}
		return 1;
	}

	public int printGraph() {
		if(graph.n_nodes <= 0 || graph.n_edges <= 0) {
			return -1;
		}
		else {
			for(int i = 0; i < graph.n_nodes; i++) {
				print("Node " + i + ": " + graph.nodes[i].id );//+ ", " + graph.nodes[i].label);
			}
			for(int x = 0; x < graph.n_edges; x++) {
				print("Edge " + x + ": " + graph.edges[x].source + ", " + graph.edges[x].target);
			}
		}
		return 1;
	}
}
