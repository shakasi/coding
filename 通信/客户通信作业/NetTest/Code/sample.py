import urllib
import urllib2
import json

def http_login():
	url='http://192.168.1.37/xhrlogin.jsp'
	values={'cookie':0,'username':'admin','password':'1234567!'}

	jdata = json.dumps(values)
	print jdata
	req = urllib2.Request(url,jdata)
	response = urllib2.urlopen(req)
	return response.read()

resp = http_login()
print resp
data = json.loads(resp)

def outlet_ctrl():
	url='http://192.168.1.37/xhroutpowstatset.jsp'
	values={'cookie':data['cookie'],'outlet1':3,'outlet2':0,'pduid':1,'powstat':1}
	jdata = json.dumps(values)
	print jdata
	req = urllib2.Request(url,jdata)
	response = urllib2.urlopen(req)
	return response.read()

resp = outlet_ctrl()
print resp

def outlet_info():
	url='http://192.168.1.37/xhroutget.jsp'
	values={'cookie':data['cookie'],'pduid':1,'id':4}

	jdata = json.dumps(values)
	print jdata
	req = urllib2.Request(url,jdata)
	response = urllib2.urlopen(req)
	return response.read()

resp = outlet_info()
print resp